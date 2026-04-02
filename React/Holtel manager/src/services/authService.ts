import api from './api';

interface LoginData {
  username: string;
  password: string;
}

interface RegisterData {
  username: string;
  email: string;
  password: string;
}

interface AuthResponse {
  userName: string;
  email: string;
  token: string;
}

const authService = {
  login: async (data: LoginData): Promise<AuthResponse> => {
    const response = await api.post('/Account/login', data);
    return response.data;
  },

  register: async (data: RegisterData): Promise<AuthResponse> => {
    const response = await api.post('/Account/register', data);
    return response.data;
  },

  logout: () => {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  },

  getCurrentUser: async () => {
    const response = await api.get('/Account/me');
    return response.data;
  },
};

export { authService };
export type { LoginData, RegisterData, AuthResponse };
