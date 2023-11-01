import axios from 'axios';
import { API_URL } from '../constants/apiConfig';

export const danhSachActor = (soLuong: string) => {
  return axios.get(`${API_URL}/actor/danhsach/${soLuong}`).then((response) => {
    return response.data;
  });
};

export const getActorDetail = (index: string) => {
  return axios.get(`${API_URL}/actor/chitiet/${index}`).then((response) => {
    return response.data;
  });
};