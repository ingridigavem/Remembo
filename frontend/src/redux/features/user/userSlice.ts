

import { RootState } from "@/redux/store";
import { createSlice } from "@reduxjs/toolkit";
import { jwtDecode } from "jwt-decode";

export interface UserState {
    user: User | null
}

const initialState: UserState = {
    user: null
}

export const userSlice = createSlice({
    name: "user",
    initialState,
    reducers: {
        login: (state, action) => {
            const token = action.payload
            const user = jwtDecode<User>(token);
            localStorage.setItem(process.env.REACT_APP_TOKEN_KEY ?? "", token)
            state.user = {...user}
        },
        logout: (state) => {
            localStorage.removeItem(process.env.REACT_APP_TOKEN_KEY ?? "")
            state.user = null
        }
    }
})

export const { login, logout } = userSlice.actions;

export const selectUser = (state: RootState) => state.userReducer.user

export default userSlice.reducer
