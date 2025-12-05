import React from "react";
import { render, screen, fireEvent, waitFor } from "@testing-library/react";
import AddBook from "./AddBook";

// Mock localStorage
beforeEach(() => {
  Storage.prototype.getItem = jest.fn(() => "fake-token");
});

// Mock fetch globally
global.fetch = jest.fn();

describe("AddBook Component", () => {
  test("renders all input fields and button", () => {
    render(<AddBook />);

    expect(screen.getByText(/ADD BOOK TO LIBRARY/i)).toBeInTheDocument();
    expect(screen.getByPlaceholderText(/Book Name/i)).toBeInTheDocument();
    expect(screen.getByPlaceholderText(/Author Name/i)).toBeInTheDocument();
    expect(screen.getByPlaceholderText(/Copies/i)).toBeInTheDocument();
    expect(screen.getByRole("button", { name: /ADD/i })).toBeInTheDocument();
  });

  test("allows user to type in inputs", () => {
    render(<AddBook />);

    fireEvent.change(screen.getByPlaceholderText(/Book Name/i), {
      target: { value: "Test Book" },
    });

    fireEvent.change(screen.getByPlaceholderText(/Author Name/i), {
      target: { value: "John Doe" },
    });

    fireEvent.change(screen.getByPlaceholderText(/Copies/i), {
      target: { value: "5" },
    });

    expect(screen.getByPlaceholderText(/Book Name/i).value).toBe("Test Book");
    expect(screen.getByPlaceholderText(/Author Name/i).value).toBe("John Doe");
    expect(screen.getByPlaceholderText(/Copies/i).value).toBe("5");
  });

  test("shows success message on successful add", async () => {
    // Mock fetch success
    fetch.mockResolvedValueOnce({
      ok: true,
      json: async () => ({ id: 1, name: "Test Book" }),
    });

    render(<AddBook />);

    fireEvent.change(screen.getByPlaceholderText(/Book Name/i), {
      target: { value: "Book A" },
    });
    fireEvent.change(screen.getByPlaceholderText(/Author Name/i), {
      target: { value: "Author A" },
    });
    fireEvent.change(screen.getByPlaceholderText(/Copies/i), {
      target: { value: "3" },
    });

    fireEvent.click(screen.getByRole("button", { name: /ADD/i }));

    const successMessage = await screen.findByText(/Book added successfully!!/i);
    expect(successMessage).toBeInTheDocument();
  });

  test("shows error message on failed response", async () => {
    fetch.mockResolvedValueOnce({
      ok: false,
      statusText: "Bad Request",
      json: async () => ({ error: "Invalid" }),
    });

    render(<AddBook />);

    fireEvent.change(screen.getByPlaceholderText(/Book Name/i), {
      target: { value: "Invalid" },
    });
    fireEvent.change(screen.getByPlaceholderText(/Author Name/i), {
      target: { value: "Error" },
    });
    fireEvent.change(screen.getByPlaceholderText(/Copies/i), {
      target: { value: "0" },
    });

    fireEvent.click(screen.getByRole("button", { name: /ADD/i }));

    const errorMessage = await screen.findByText(/Response:Bad Request/i);
    expect(errorMessage).toBeInTheDocument();
  });

  test("clears input fields after success", async () => {
    fetch.mockResolvedValueOnce({
      ok: true,
      json: async () => ({ id: 1 })
    });

    render(<AddBook />);

    fireEvent.change(screen.getByPlaceholderText(/Book Name/i), {
      target: { value: "Book ABC" },
    });
    fireEvent.change(screen.getByPlaceholderText(/Author Name/i), {
      target: { value: "XYZ" },
    });
    fireEvent.change(screen.getByPlaceholderText(/Copies/i), {
      target: { value: "10" },
    });

    fireEvent.click(screen.getByRole("button", { name: /ADD/i }));

    await screen.findByText(/Book added successfully!!/i);

    expect(screen.getByPlaceholderText(/Book Name/i).value).toBe("");
    expect(screen.getByPlaceholderText(/Author Name/i).value).toBe("");
    expect(screen.getByPlaceholderText(/Copies/i).value).toBe("");
  });
});
