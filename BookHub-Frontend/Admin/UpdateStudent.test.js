import React from "react";
import { render, screen, fireEvent, waitFor } from "@testing-library/react";
import UpdateUser from "./UpdateStudent";

// Mock localStorage
beforeEach(() => {
  Storage.prototype.getItem = jest.fn(() => "fake-token");
});

// Mock fetch
global.fetch = jest.fn();

// Mock confirm dialog
global.confirm = jest.fn();

describe("UpdateUser Component", () => {

  afterEach(() => {
    jest.clearAllMocks();
  });

  test("renders the component with required elements", () => {
    render(<UpdateUser />);
    expect(screen.getByText(/UPDATE PROFILE/i)).toBeInTheDocument();
    expect(screen.getByPlaceholderText(/Enter Student ID to update/i)).toBeInTheDocument();
    expect(screen.getByRole("button", { name: /Fetch data/i })).toBeInTheDocument();
    expect(screen.getByRole("button", { name: /Update/i })).toBeDisabled();
    expect(screen.getByRole("button", { name: /Delete/i })).toBeDisabled();
  });

  test("fetches student data successfully", async () => {
    fetch.mockResolvedValueOnce({
      ok: true,
      json: async () => ({
        studentName: "John",
        year: "2",
        department: "CSE",
        email: "john@example.com",
        phone: "9876543210",
        password: "secret123"
      })
    });

    render(<UpdateUser />);

    fireEvent.change(screen.getByPlaceholderText(/Enter Student ID/i), { target: { value: "2" } });
    fireEvent.click(screen.getByText(/Fetch data/i));

    await waitFor(() => expect(screen.getByDisplayValue("John")).toBeInTheDocument());
    expect(screen.getByDisplayValue("john@example.com")).toBeInTheDocument();
    expect(screen.getByDisplayValue("9876543210")).toBeInTheDocument();

    // Buttons should now be enabled
    expect(screen.getByRole("button", { name: /Update/i })).toBeEnabled();
    expect(screen.getByRole("button", { name: /Delete/i })).toBeEnabled();
  });

  test("shows error message if fetchData fails", async () => {
    fetch.mockResolvedValueOnce({
      ok: false,
      status: 404
    });

    render(<UpdateUser />);
    fireEvent.change(screen.getByPlaceholderText(/Enter Student ID/i), { target: { value: "999" } });
    fireEvent.click(screen.getByText(/Fetch data/i));

    const errorMessage = await screen.findByText(/Error occured 404/i);
    expect(errorMessage).toBeInTheDocument();
  });

  test("updates a user successfully", async () => {
    // Mock fetch: first fetchData, then handleUpdate
    fetch
      .mockResolvedValueOnce({
        ok: true,
        json: async () => ({
          studentName: "Arya",
          year: "3",
          department: "ECE",
          email: "arya@example.com",
          phone: "9994443333",
          password: "noone"
        })
      })
      .mockResolvedValueOnce({
        ok: true,
        json: async () => ({
          studentName: "Arya",
        })
      });

    render(<UpdateUser />);
    fireEvent.change(screen.getByPlaceholderText(/Enter Student ID/i), { target: { value: "5" } });
    fireEvent.click(screen.getByText(/Fetch data/i));

    await screen.findByDisplayValue("Arya");

    fireEvent.change(screen.getByPlaceholderText(/Name/i), { target: { value: "Arya of Winterfell" } });
    fireEvent.click(screen.getByRole("button", { name: /Update/i }));

    const successMsg = await screen.findByText(/Profile updated successfully/i);
    expect(successMsg).toBeInTheDocument();
  });

  test("shows update error if PATCH fails", async () => {
    fetch
      .mockResolvedValueOnce({
        ok: true,
        json: async () => ({
          studentName: "B",
          year: "1",
          department: "CSE",
          email: "bran@example.com",
          phone: "1234567890",
          password: "threeeyed"
        })
      })
      .mockResolvedValueOnce({
        ok: false,
        json: async () => ({
          Error: ["Invalid email format"]
        })
      });

    render(<UpdateUser />);
    fireEvent.change(screen.getByPlaceholderText(/Enter Student ID/i), { target: { value: "20" } });
    fireEvent.click(screen.getByText(/Fetch data/i));

    await screen.findByDisplayValue("B");

    fireEvent.click(screen.getByRole("button", { name: /Update/i }));

    const errorMsg = await screen.findByText("Invalid email format");
    expect(errorMsg).toBeInTheDocument();
  });

  test("handles delete operation with confirm", async () => {
    global.confirm = jest.fn(() => true);

    fetch
      .mockResolvedValueOnce({
        ok: true,
        json: async () => ({
          studentName: "J",
          year: "2",
          department: "CSE",
          email: "john@example.com",
          phone: "9876543210",
          password: "secret123"
        })
      })
      .mockResolvedValueOnce({
        ok: true,
        text: async () => "Student deleted successfully"
      });

    render(<UpdateUser />);

    fireEvent.change(screen.getByPlaceholderText(/Enter Student ID/i), { target: { value: "2" } });
    fireEvent.click(screen.getByText(/Fetch data/i));

    await screen.findByDisplayValue("J");

    // Click delete
    fireEvent.click(screen.getByRole("button", { name: /Delete/i }));

    const successMsg = await screen.findByText((content) =>
      content.includes("Student deleted successfully")
    );
    expect(successMsg).toBeInTheDocument();

    // After delete, form fields should be cleared
    expect(screen.getByPlaceholderText(/Name/i).value).toBe("");
    expect(screen.getByPlaceholderText(/Email/i).value).toBe("");
  });

  test("does nothing if delete is cancelled", async () => {
  global.confirm = jest.fn(() => false);

  // Mock fetch for fetchData
  fetch.mockResolvedValueOnce({
    ok: true,
    json: async () => ({
      studentName: "J",
      year: "2",
      department: "CSE",
      email: "john@example.com",
      phone: "9876543210",
      password: "secret123"
    })
  });

  render(<UpdateUser />);

  fireEvent.change(screen.getByPlaceholderText(/Enter Student ID/i), { target: { value: "2" } });
  fireEvent.click(screen.getByText(/Fetch data/i));

  // Wait for form to populate
  await screen.findByDisplayValue("J");

  fireEvent.click(screen.getByRole("button", { name: /Delete/i }));

  // DELETE should not be called
  expect(fetch).toHaveBeenCalledTimes(1); // Only the GET fetchData call
});
});
