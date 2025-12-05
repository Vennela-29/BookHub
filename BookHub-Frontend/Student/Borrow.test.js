import React from "react";
import { render, screen, fireEvent, waitFor } from "@testing-library/react";
import Borrow from "./Borrow";

// Mock localStorage
beforeEach(() => {
  localStorage.setItem("token", "mock-token");
  localStorage.setItem("studentId", "1");
});

afterEach(() => {
  jest.restoreAllMocks();
  localStorage.clear();
});

describe("Borrow Component", () => {
  const mockBooks = [
    { bookId: 1, bookName: "React Basics", author: "John Doe", availableCopies: 3 },
    { bookId: 2, bookName: "JS Guide", author: "Jane Smith", availableCopies: 2 },
  ];

  test("renders input, button and table", async () => {
    // Mock fetchBooks
    jest.spyOn(global, "fetch").mockResolvedValueOnce({
      ok: true,
      json: jest.fn().mockResolvedValue(mockBooks),
    });

    render(<Borrow />);

    expect(screen.getByPlaceholderText(/Enter Book Name/i)).toBeInTheDocument();
    expect(screen.getByRole("button", { name: /Borrow/i })).toBeInTheDocument();

    await waitFor(() => {
      expect(screen.getByText(/React Basics/i)).toBeInTheDocument();
      expect(screen.getByText(/JS Guide/i)).toBeInTheDocument();
    });
  });

  test("borrows a book successfully", async () => {
    const borrowMessage = "Book borrowed successfully";

    // First fetchBooks on mount
    jest.spyOn(global, "fetch")
      .mockResolvedValueOnce({
        ok: true,
        json: jest.fn().mockResolvedValue(mockBooks),
      })
      // Borrow API call
      .mockResolvedValueOnce({
        ok: true,
        text: jest.fn().mockResolvedValue(borrowMessage),
      })
      // fetchBooks after borrow
      .mockResolvedValueOnce({
        ok: true,
        json: jest.fn().mockResolvedValue(mockBooks),
      });

    render(<Borrow />);

    const input = screen.getByPlaceholderText(/Enter Book Name/i);
    const button = screen.getByRole("button", { name: /Borrow/i });

    fireEvent.change(input, { target: { value: "React Basics" } });
    fireEvent.click(button);

    await waitFor(() => {
      expect(screen.getByText(borrowMessage)).toBeInTheDocument();
      expect(input.value).toBe(""); // input cleared
    });
  });

  test("shows error message if borrow fails", async () => {
    const errorMessage = "Cannot borrow book";

    // First fetchBooks on mount
    jest.spyOn(global, "fetch")
      .mockResolvedValueOnce({
        ok: true,
        json: jest.fn().mockResolvedValue(mockBooks),
      })
      // Borrow API fails
      .mockResolvedValueOnce({
        ok: false,
        text: jest.fn().mockResolvedValue(errorMessage),
      });

    render(<Borrow />);

    fireEvent.change(screen.getByPlaceholderText(/Enter Book Name/i), {
      target: { value: "JS Guide" },
    });

    fireEvent.click(screen.getByRole("button", { name: /Borrow/i }));

    await waitFor(() => {
      expect(screen.getByText(errorMessage)).toBeInTheDocument();
    });
  });
});
