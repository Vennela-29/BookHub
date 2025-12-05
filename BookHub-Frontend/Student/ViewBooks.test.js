import React from "react";
import { render, screen, fireEvent, waitFor } from "@testing-library/react";
import ViewBooks from "./ViewBooks";

// Mock localStorage
beforeEach(() => {
  localStorage.setItem("token", "mock-token");
});

afterEach(() => {
  jest.restoreAllMocks();
  localStorage.clear();
});

describe("ViewBooks Component", () => {
  const mockBooks = [
    { bookId: 1, bookName: "React Basics", author: "John Doe", availableCopies: 3 },
    { bookId: 2, bookName: "JS Guide", author: "Jane Smith", availableCopies: 2 },
  ];

  test("renders books after fetching", async () => {
    jest.spyOn(global, "fetch").mockResolvedValueOnce({
      ok: true,
      json: jest.fn().mockResolvedValue(mockBooks),
    });

    render(<ViewBooks />);

    await waitFor(() => {
      expect(screen.getByText("React Basics")).toBeInTheDocument();
      expect(screen.getByText("JS Guide")).toBeInTheDocument();
    });

    expect(screen.getByText("Book ID")).toBeInTheDocument();
    expect(screen.getByText("Author")).toBeInTheDocument();
  });

  test("filters books using the search input", async () => {
    jest.spyOn(global, "fetch").mockResolvedValueOnce({
      ok: true,
      json: jest.fn().mockResolvedValue(mockBooks),
    });

    render(<ViewBooks />);

    await waitFor(() => screen.getByText("React Basics"));

    const input = screen.getByPlaceholderText(/Search Book or Author/i);

    // Filter to "JS Guide"
    fireEvent.change(input, { target: { value: "JS Guide" } });
    await waitFor(() => {
      expect(screen.getByText("JS Guide")).toBeInTheDocument();
      expect(screen.queryByText("React Basics")).not.toBeInTheDocument();
    });

    // Filter to a non-existent book
    fireEvent.change(input, { target: { value: "Python" } });
    await waitFor(() => {
      expect(screen.getByText("Book or Author not found")).toBeInTheDocument();
    });
  });

  test("logs error when fetch fails", async () => {
    const consoleErrorSpy = jest
      .spyOn(console, "error")
      .mockImplementation(() => {});

    // Mock fetch to throw
    jest.spyOn(global, "fetch").mockImplementationOnce(() =>
      Promise.reject(new Error("API failure"))
    );

    render(<ViewBooks />);

    await waitFor(() => {
      expect(consoleErrorSpy).toHaveBeenCalled();
      expect(consoleErrorSpy).toHaveBeenCalledWith(
        expect.stringContaining("Error Occured while fetching Books")
      );
    });

    consoleErrorSpy.mockRestore();
  });

  test("shows 'No books Found' when API returns empty array", async () => {
    jest.spyOn(global, "fetch").mockResolvedValueOnce({
      ok: true,
      json: jest.fn().mockResolvedValue([]),
    });

    render(<ViewBooks />);

    await waitFor(() => {
      expect(screen.getByText("No books Found")).toBeInTheDocument();
    });
  });
});
