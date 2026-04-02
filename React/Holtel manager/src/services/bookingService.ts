import api from './api';

export interface Booking {
  id: number;
  roomId: number;
  userId: string;
  checkInDate: string;
  checkOutDate: string;
  numberOfGuests: number;
  totalPrice: number;
  status: string;
  specialRequests?: string;
}

export interface CreateBookingData {
  roomId: number;
  checkInDate: string;
  checkOutDate: string;
  numberOfGuests: number;
  specialRequests?: string;
}

export const bookingService = {
  getMyBookings: async (): Promise<Booking[]> => {
    const response = await api.get('/Booking/my-bookings');
    return response.data;
  },

  create: async (data: CreateBookingData): Promise<Booking> => {
    const response = await api.post('/Booking', data);
    return response.data;
  },

  cancel: async (id: number) => {
    const response = await api.delete(`/Booking/${id}`);
    return response.data;
  },
};
