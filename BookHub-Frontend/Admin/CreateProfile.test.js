import React from "react";
import { render, screen, fireEvent, waitFor } from "@testing-library/react";
import CreateUser from "./CreateProfile";
import { MemoryRouter } from "react-router-dom";

// Mock localStorage
beforeEach(() => {
  localStorage.setItem("token", "mock-token");
});

afterEach(() => {
  jest.restoreAllMocks();
  localStorage.clear();
});

describe("CreateUser Component", () => {

  test("renders form fields correctly", () => {
    render(<CreateUser />, { wrapper: MemoryRouter });

    expect(screen.getByPlaceholderText("Name")).toBeInTheDocument();
    expect(screen.getByPlaceholderText("Email")).toBeInTheDocument();
    expect(screen.getByPlaceholderText("Phone no")).toBeInTheDocument();
    expect(screen.getByPlaceholderText("Password")).toBeInTheDocument();
    expect(screen.getByLabelText(/Year/i)).toHaveDisplayValue("--Select--");
expect(screen.getByLabelText(/Department/i)).toHaveDisplayValue("--Select--");
    expect(screen.getByRole("button", { name: /submit/i })).toBeInTheDocument();
  });

  test("submits form successfully", async () => {
    const mockResponse = {
      id: 1,
      studentName: "Alice Johnson",
      email: "alice@example.com"
    };

    jest.spyOn(global, "fetch").mockResolvedValueOnce({
      ok: true,
      json: jest.fn().mockResolvedValue(mockResponse),
    });

    render(<CreateUser />, { wrapper: MemoryRouter });

    fireEvent.change(screen.getByPlaceholderText("Name"), { target: { value: "Alice Johnson" } });
    fireEvent.change(screen.getByPlaceholderText("Email"), { target: { value: "alice@example.com" } });
    fireEvent.change(screen.getByPlaceholderText("Phone no"), { target: { value: "1234567890" } });
    fireEvent.change(screen.getByPlaceholderText("Password"), { target: { value: "password123" } });
   fireEvent.change(screen.getByLabelText(/Year/i), { target: { value: "1" } });
fireEvent.change(screen.getByLabelText(/Department/i), { target: { value: "CSE" } });

    fireEvent.click(screen.getByRole("button", { name: /submit/i }));

    await waitFor(() => {
      expect(screen.getByText("Profile created successfully!!")).toBeInTheDocument();
    });
  });

  test("shows API error message", async () => {
    jest.spyOn(global, "fetch").mockResolvedValueOnce({
      ok: false,
      json: jest.fn().mockResolvedValue({ Error: ["Email already exists"] }),
    });

    render(<CreateUser />, { wrapper: MemoryRouter });

    fireEvent.change(screen.getByPlaceholderText(/Name/i), { target: { value: /Alice/i } });
    fireEvent.change(screen.getByPlaceholderText("Email"), { target: { value: /alice@example.com/i } });
    fireEvent.change(screen.getByPlaceholderText("Phone no"), { target: { value: "1234567890" } });
    fireEvent.change(screen.getByPlaceholderText("Password"), { target: { value: "password123" } });
    fireEvent.change(screen.getByLabelText(/Year/i), { target: { value: "1" } });
fireEvent.change(screen.getByLabelText(/Department/i), { target: { value: "CSE" } });

    fireEvent.click(screen.getByRole("button", { name: /submit/i }));

    await waitFor(() => {
      expect(screen.getByText("Email already exists")).toBeInTheDocument();
    });
  });

  test("shows validation errors for phone and password", () => {
    render(<CreateUser />, { wrapper: MemoryRouter });

    fireEvent.change(screen.getByPlaceholderText("Phone no"), { target: { value: "12345" } });
    expect(screen.getByText(/Phone number must be 10 digits/i)).toBeInTheDocument();

    fireEvent.change(screen.getByPlaceholderText("Password"), { target: { value: "123" } });
    expect(screen.getByText(/Password must be atleast 6 and atmost 20 characters/i)).toBeInTheDocument();
  });

});
