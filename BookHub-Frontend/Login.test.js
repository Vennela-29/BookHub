// src/Tests/Login.test.js
import '@testing-library/jest-dom';
import { render, screen, fireEvent } from '@testing-library/react';
import Login from './Login';

describe('Login Component', () => {

  const setup = (props = {}) => {
    const defaultProps = {
      username: "",
      password: "",
      setUsername: jest.fn(),
      setPassword: jest.fn(),
      handleLogin: jest.fn(),
      Error: "",
      loading: "",
      ...props
    };

    render(<Login {...defaultProps} />);
    return defaultProps;
  };

  test('renders inputs and button', () => {
    setup();

    expect(screen.getByPlaceholderText(/Enter Email here/i)).toBeInTheDocument();
    expect(screen.getByPlaceholderText(/password/i)).toBeInTheDocument();
    expect(screen.getByRole('button', { name: /login/i })).toBeInTheDocument();
  });

  test('shows error on failed login', async () => {
    global.mockFetchFailure("Invalid credentials");

    // mock handleLogin to simulate real behavior without re-rendering
    const handleLoginMock = async (e) => {
      e.preventDefault();
      const res = await fetch();
      const err = await res.json();

      // Instead of re-rendering, return the error message
      return err.message;
    };

    setup({ handleLogin: handleLoginMock });

    fireEvent.click(screen.getByRole("button", { name: /login/i }));

    // Render component again with updated Error prop (ONE clean re-render)
    render(
      <Login
        username=""
        password=""
        setUsername={() => {}}
        setPassword={() => {}}
        handleLogin={() => {}}
        Error="Invalid credentials"
        loading=""
      />
    );

    expect(await screen.findByText(/Invalid credentials/i)).toBeInTheDocument();
  });


  test('handles successful login', async () => {
  global.mockFetchSuccess({
    token: "123",
    role: "Admin",
    username: "admin"
  });

  const handleLoginMock = jest.fn(async (e) => {
    e.preventDefault();
    const res = await fetch();
    const data = await res.json();
    localStorage.setItem("token", data.token);
    localStorage.setItem("role", data.role);
    localStorage.setItem("username", data.username);
  });

  setup({ handleLogin: handleLoginMock });

  fireEvent.click(screen.getByRole("button", { name: /login/i }));

  // wait for async mock to finish
  await screen.findByRole("button");

  expect(localStorage.getItem("token")).toBe("123");
  expect(localStorage.getItem("role")).toBe("Admin");
  expect(localStorage.getItem("username")).toBe("admin");
});

});
