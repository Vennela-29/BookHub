import { render, screen, fireEvent } from "@testing-library/react";
import Search from "./Search";

describe("Search Component", () => {
  const sampleBooks = [
    { bookName: "JavaScript", author: "John Doe" },
    { bookName: "React", author: "Dan Abramov" },
    { bookName: "Python", author: "Guido" },
  ];

  test("renders search input", () => {
    render(<Search books={sampleBooks} onFilter={jest.fn()} />);

    expect(
      screen.getByPlaceholderText(/Search Book or Author/i)
    ).toBeInTheDocument();
  });

  test("filters books when typing", () => {
    const mockFilter = jest.fn();
    render(<Search books={sampleBooks} onFilter={mockFilter} />);

    const input = screen.getByPlaceholderText(/Search Book or Author/i);

    // User types "React"
    fireEvent.change(input, { target: { value: "React" } });

    expect(mockFilter).toHaveBeenCalled();
    expect(mockFilter).toHaveBeenCalledWith([
      { bookName: "React", author: "Dan Abramov" },
    ]);
  });

  test("filters by author name", () => {
    const mockFilter = jest.fn();
    render(<Search books={sampleBooks} onFilter={mockFilter} />);

    const input = screen.getByPlaceholderText(/Search Book or Author/i);

    fireEvent.change(input, { target: { value: "guido" } });

    expect(mockFilter).toHaveBeenCalledWith([
      { bookName: "Python", author: "Guido" },
    ]);
  });

  test("shows error message if filtering throws an error", () => {
    // Passing invalid books list to force an error
    const brokenBooks = null;

    render(<Search books={brokenBooks} onFilter={jest.fn()} />);

    const input = screen.getByPlaceholderText(/Search Book or Author/i);

    fireEvent.change(input, { target: { value: "test" } });

    expect(screen.getByText(/Error occured/i)).toBeInTheDocument();
  });
});
