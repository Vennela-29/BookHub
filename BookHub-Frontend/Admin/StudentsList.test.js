import React from "react";
import { render, screen, waitFor } from "@testing-library/react";
import AllUsers from "./StudentsList";

describe("AllUsers Component", () => {
  beforeEach(() => {
    localStorage.setItem("token", "mock-token");
  });

  afterEach(() => {
    jest.restoreAllMocks();
    localStorage.clear();
  });

  test("shows error message on fetch failure", async () => {
    const consoleErrorSpy = jest.spyOn(console, "error").mockImplementation(() => {});

   jest.spyOn(global, "fetch").mockImplementationOnce(() =>
  Promise.resolve({
    ok: false,
    status: 500,
    json: () => Promise.resolve({}),
  })
);

    render(<AllUsers />);

    await waitFor(() => {
  expect(screen.getByText(/Error 500/i)).toBeInTheDocument();
});
    consoleErrorSpy.mockRestore();
  });

  test("renders students after successful fetch", async () => {
    const mockStudents = [
      { id: 1, studentName: "John", year: "2", department: "CSE", email: "john@example.com", phone: "1234567890", borrowedBooks: ["Book A"], date2: ["2025-12-05"] },
      { id: 2, studentName: "Alice", year: "3", department: "ECE", email: "alice@example.com", phone: "9876543210", borrowedBooks: ["Book B"], date2: ["2025-12-10"] },
    ];

    jest.spyOn(global, "fetch").mockResolvedValueOnce({
      ok: true,
      json: jest.fn().mockResolvedValue(mockStudents),
    });

    render(<AllUsers />);

    await waitFor(() => {
      const nameCells = screen.getAllByText(/John/i);
      expect(nameCells[0]).toBeInTheDocument();
      const nameCell2 = screen.getAllByText(/Alice/i);
      expect(nameCell2[0]).toBeInTheDocument();
    });
  });
});
