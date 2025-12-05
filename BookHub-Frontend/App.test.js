// src/App.test.js
import React from 'react';
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import App from './App';
import { MemoryRouter } from 'react-router-dom';

// Mock useNavigate from react-router-dom
const mockedNavigate = jest.fn();

jest.mock('react-router-dom', () => {
  const originalModule = jest.requireActual('react-router-dom');
  return {
    ...originalModule,
    useNavigate: () => mockedNavigate,
  };
});

beforeEach(() => {
  localStorage.clear();
  mockedNavigate.mockReset();

  // Reset global fetch
  global.fetch = jest.fn();
});

test('renders login form', () => {
  render(
    <MemoryRouter>
      <App />
    </MemoryRouter>
  );

  expect(screen.getByPlaceholderText(/Enter Email here/i)).toBeInTheDocument();
  expect(screen.getByPlaceholderText(/password/i)).toBeInTheDocument();
  expect(screen.getByRole('button', { name: /login/i })).toBeInTheDocument();
});

test('shows error on failed login', async () => {
  render(
    <MemoryRouter>
      <App />
    </MemoryRouter>
  );

  // Mock failed fetch response
  global.fetch.mockResolvedValueOnce({
    ok: false,
    json: async () => ({ message: 'Invalid credentials' }),
  });

  fireEvent.change(screen.getByPlaceholderText(/Enter Email here/i), {
    target: { value: 'wronguser' },
  });
  fireEvent.change(screen.getByPlaceholderText(/password/i), {
    target: { value: 'wrongpass' },
  });

  fireEvent.click(screen.getByRole('button', { name: /login/i }));

  const errorMessage = await screen.findByText(/Invalid credentials/i);
  expect(errorMessage).toBeInTheDocument();
});

test('successful login navigates to correct route', async () => {
  render(
    <MemoryRouter>
      <App />
    </MemoryRouter>
  );

  // Mock successful fetch response
  global.fetch.mockResolvedValueOnce({
    ok: true,
    json: async () => ({
      token: '123',
      role: 'Admin',
      username: 'adminuser',
    }),
  });

  fireEvent.change(screen.getByPlaceholderText(/Enter Email here/i), {
    target: { value: 'adminuser' },
  });
  fireEvent.change(screen.getByPlaceholderText(/password/i), {
    target: { value: 'adminpass' },
  });

  fireEvent.click(screen.getByRole('button', { name: /login/i }));

  // Wait for navigation and localStorage updates
  await waitFor(() => {
    expect(localStorage.getItem('token')).toBe('123');
    expect(localStorage.getItem('role')).toBe('Admin');
    expect(localStorage.getItem('username')).toBe('adminuser');
    expect(mockedNavigate).toHaveBeenCalledWith('./Admin/AdminDashboard');
  });
});

test('successful student login navigates to student route', async () => {
  render(
    <MemoryRouter>
      <App />
    </MemoryRouter>
  );

  // Mock successful fetch response for student
  global.fetch.mockResolvedValueOnce({
    ok: true,
    json: async () => ({
      token: '456',
      role: 'Student',
      username: 'studentuser',
    }),
  });

  fireEvent.change(screen.getByPlaceholderText(/Enter Email here/i), {
    target: { value: 'studentuser' },
  });
  fireEvent.change(screen.getByPlaceholderText(/password/i), {
    target: { value: 'studentpass' },
  });

  fireEvent.click(screen.getByRole('button', { name: /login/i }));

  await waitFor(() => {
    expect(localStorage.getItem('token')).toBe('456');
    expect(localStorage.getItem('role')).toBe('Student');
    expect(localStorage.getItem('username')).toBe('studentuser');
    expect(mockedNavigate).toHaveBeenCalledWith('./Student/Students');
  });
});
