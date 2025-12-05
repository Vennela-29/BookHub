// import '@testing-library/jest-dom/extend-expect';
import '@testing-library/jest-dom';
// import { jest } from '@jest/globals';

//Creating Fake interface to act like local storage
class LocalStorageMock {
  constructor() { this.store = {}; }
  getItem(key) { return this.store[key] || null; }
  setItem(key, value) { this.store[key] = value.toString(); }
  removeItem(key) { delete this.store[key]; }
  clear() { this.store = {}; }
}
//Acts like local storage
global.localStorage = new LocalStorageMock();

//Act like fetch API to fetch responses
global.fetch = jest.fn((url, options) => {
  // Default: return failure
  return Promise.resolve({
    ok: false,
    json: () => Promise.resolve({ message: 'Mocked fetch default failure' }),
  });
});

// Helper functions for tests
global.mockFetchSuccess = (data) => {
  global.fetch.mockImplementationOnce(() =>
    Promise.resolve({ ok: true,status:200 ,json: () => Promise.resolve(data) })
  );
};
global.mockFetchFailure = (message) => {
  global.fetch.mockImplementationOnce(() =>
    Promise.resolve({ ok: false, status:400,json: () => Promise.resolve({ message }) })
  );
};
