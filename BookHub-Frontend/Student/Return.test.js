import React from "react";
import { render, screen, fireEvent, waitFor } from "@testing-library/react";
import Return from "./Return";

// Mock localStorage
beforeEach(() => {
  localStorage.setItem("token", "mock-token");
  localStorage.setItem("studentId", "1");
  localStorage.setItem(
    "Books",
    JSON.stringify(["Book A - Author A", "Book B - Author B"])
  );
});

// Clean up mocks
afterEach(() => {
  jest.restoreAllMocks();
  localStorage.clear();
});

describe("Return Component", () => {
  test("renders borrowed books from localStorage", () => {
    render(<Return />);

    expect(screen.getByText("Book A - Author A")).toBeInTheDocument();
    expect(screen.getByText("Book B - Author B")).toBeInTheDocument();
    expect(screen.getByRole("button", { name: /Return/i })).toBeDisabled(); // initially disabled
  });


  test("returns a book successfully", async () => {
    const confirmSpy = jest.spyOn(window, "confirm").mockReturnValue(true);
    const returnMessage = "Book returned successfully";

    // Mock fetch for returning book
    jest.spyOn(global, "fetch").mockResolvedValueOnce({
      ok: true,
      text: jest.fn().mockResolvedValue(returnMessage),
    });

    render(<Return />);

    // Select a book
    fireEvent.click(screen.getByLabelText("Book A - Author A"));

    const button = screen.getByRole("button", { name: /Return/i });
    fireEvent.click(button);

    await waitFor(() => {
      expect(screen.getByText(returnMessage)).toBeInTheDocument();
      // The selected book should be removed
      expect(screen.queryByText("Book A - Author A")).not.toBeInTheDocument();
    });

    // Check localStorage is updated
    const storedBooks = JSON.parse(localStorage.getItem("Books"));
    expect(storedBooks).toEqual(["Book B - Author B"]);

    confirmSpy.mockRestore();
  });

  test("does not return book if confirm is cancelled", async () => {
    jest.spyOn(window, "confirm").mockReturnValue(false);

    render(<Return />);

    fireEvent.click(screen.getByLabelText("Book A - Author A"));
    fireEvent.click(screen.getByRole("button", { name: /Return/i }));

    await waitFor(() => {
      // Message should not appear
      expect(screen.queryByText(/Book returned successfully/i)).not.toBeInTheDocument();
      expect(screen.getByText("Book A - Author A")).toBeInTheDocument();
    });
  });

  test("shows error if return fails", async () => {
    jest.spyOn(window, "confirm").mockReturnValue(true);

    const errorMessage = "Failed to return book";
    jest.spyOn(global, "fetch").mockResolvedValueOnce({
      ok: false,
      text: jest.fn().mockResolvedValue(errorMessage),
    });

    render(<Return />);

    fireEvent.click(screen.getByLabelText("Book A - Author A"));
    fireEvent.click(screen.getByRole("button", { name: /Return/i }));

    await waitFor(() => {
      expect(screen.getByText(`Failed to return book: ${errorMessage}`)).toBeInTheDocument();
    });
  });

});
