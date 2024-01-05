import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import { UserStorage } from "../apis/auth/user";

interface authState {
    isLoading: boolean,
    userInfo: UserStorage
}

const initialState: authState = {
    isLoading: false,
    userInfo: {
        taiKhoan: '',
        email: '',
        hoTen: '',
        role: '',
        accessToken: '',
        refreshToken: '',
        noiCongTac: [],
    }
}

export const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        setAuth: (state, action: PayloadAction<UserStorage>) => {
            state.userInfo = action.payload;
            state.isLoading = false;
        },
        clearAuth: () => initialState,
    },
})

export const { setAuth } = authSlice.actions;
export const { clearAuth } = authSlice.actions;
export default authSlice.reducer