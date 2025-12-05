import React from "react";
import { render, screen, waitFor } from "@testing-library/react";
import Profile from "./Profile";

describe("Profile Component", () => {
  beforeEach(() => {
    localStorage.setItem("token", "mock-token");
  });

  afterEach(() => {
    jest.restoreAllMocks();
    localStorage.clear();
  });

  test("renders student profile after successful fetch", async () => {
    jest.spyOn(global, "fetch").mockResolvedValueOnce({
      ok: true,
      json: jest.fn().mockResolvedValue({
        id: 10,
        name: "Alice Johnson",
        email: "alice@example.com",
        borrowedList: ["Book A", "Book B"],
      }),
    });

    render(<Profile />);

    await waitFor(() => {
      expect(screen.getByText(/Alice Johnson/i)).toBeInTheDocument();
      expect(screen.getByText(/alice@example.com/i)).toBeInTheDocument();
      expect(screen.getByText(/Book A/i)).toBeInTheDocument();
      expect(screen.getByText(/Book B/i)).toBeInTheDocument();
    });
  });

  test("logs error when fetch fails", async () => {
    const consoleErrorSpy = jest.spyOn(console, "error").mockImplementation(() => {});

    jest.spyOn(global, "fetch").mockResolvedValueOnce({
      ok: false,
      status: 404,
    });

    render(<Profile />);

    await waitFor(() => {
      expect(consoleErrorSpy).toHaveBeenCalledWith("Failed to fetch profile:404");
    });

    consoleErrorSpy.mockRestore();
  });
});
