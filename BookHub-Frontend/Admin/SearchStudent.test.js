import React from "react";
import { render, screen, fireEvent } from "@testing-library/react";
import SearchUser from "./SearchStudent";

describe("SearchUser Component", () => {
  const mockStudents = [
    {
      studentName: "Alice Johnson",
      department: "CSE",
      year: 2,
      borrowedList: ["React Guide", "Data Structures"]
    },
    {
      studentName: "Bob Smith",
      department: "ECE",
      year: 3,
      borrowedList: ["Physics Notes"]
    }
  ];

  let mockOnFilter;

  beforeEach(() => {
    mockOnFilter = jest.fn();
  });

  test("renders input field", () => {
    render(<SearchUser students={mockStudents} onFilter={mockOnFilter} />);
    expect(screen.getByPlaceholderText(/Search Student/i)).toBeInTheDocument();
  });

  test("filters by student name", () => {
    render(<SearchUser students={mockStudents} onFilter={mockOnFilter} />);

    fireEvent.change(screen.getByPlaceholderText(/Search Student/i), {
      target: { value: "alice" }
    });

    expect(mockOnFilter).toHaveBeenCalledWith([mockStudents[0]]);
  });

  test("filters by department", () => {
    render(<SearchUser students={mockStudents} onFilter={mockOnFilter} />);

    fireEvent.change(screen.getByPlaceholderText(/Search Student/i), {
      target: { value: "ece" }
    });

    expect(mockOnFilter).toHaveBeenCalledWith([mockStudents[1]]);
  });

  test("filters by year", () => {
    render(<SearchUser students={mockStudents} onFilter={mockOnFilter} />);

    fireEvent.change(screen.getByPlaceholderText(/Search Student/i), {
      target: { value: "2" }
    });

    expect(mockOnFilter).toHaveBeenCalledWith([mockStudents[0]]);
  });

  test("filters by borrowed book name", () => {
    render(<SearchUser students={mockStudents} onFilter={mockOnFilter} />);

    fireEvent.change(screen.getByPlaceholderText(/Search Student/i), {
      target: { value: "physics" }
    });

    expect(mockOnFilter).toHaveBeenCalledWith([mockStudents[1]]);
  });

  test("returns empty array if students is not an array", () => {
    render(<SearchUser students={null} onFilter={mockOnFilter} />);

    fireEvent.change(screen.getByPlaceholderText(/Search Student/i), {
      target: { value: "test" }
    });

    expect(mockOnFilter).toHaveBeenCalledWith([]);
  });

  test("calls onFilter with all students when search is empty", () => {
    render(<SearchUser students={mockStudents} onFilter={mockOnFilter} />);

    // initial run from useEffect
    expect(mockOnFilter).toHaveBeenCalledWith(mockStudents);

    fireEvent.change(screen.getByPlaceholderText(/Search Student/i), {
      target: { value: "" }
    });

    expect(mockOnFilter).toHaveBeenCalledWith(mockStudents);
  });

  test("filters correctly when search matches no user", () => {
    render(<SearchUser students={mockStudents} onFilter={mockOnFilter} />);

    fireEvent.change(screen.getByPlaceholderText(/Search Student/i), {
      target: { value: "unknown" }
    });

    expect(mockOnFilter).toHaveBeenCalledWith([]);
  });
});
