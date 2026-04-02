import api from './api';

export interface Room {
  id: number;
  roomNumber: string;
  floor: number;
  currentStatus: string;
  roomTypeId: number;
}

export const roomService = {
  getAll: async (): Promise<Room[]> => {
    const response = await api.get('/room');
    return response.data;
  },

  getById: async (id: number): Promise<Room> => {
    const response = await api.get(`/room/${id}`);
    return response.data;
  },
};
