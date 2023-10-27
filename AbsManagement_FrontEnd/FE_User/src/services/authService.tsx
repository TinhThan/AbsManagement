import axios from 'axios';
import { API_URL, USER_API_PREFIX, ACTOR_API_PREFIX } from '../constants/apiConfig';

const userLogin = (userName: string, password: string) => {
  return axios.post(`${API_URL}/${USER_API_PREFIX}/login`, { userName, password })
    .then((response) => {
      return response.data;
    });
};

export default {
  userLogin,
};