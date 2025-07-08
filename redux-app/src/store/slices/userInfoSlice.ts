import { createSlice, type PayloadAction } from '@reduxjs/toolkit';
import type { UserInfo } from '../../types/UserInfo';

export const userInfoSlice = createSlice({
    name: 'userInfo',
    initialState: {
        id: '',
        name: '',
        email: '',
        profilePictureUrl: '',
        createdAt: new Date(),
        updatedAt: new Date(),
    },
    reducers: {
        setUserInfo: (state, action: PayloadAction<UserInfo>) => {
            const { id, name, email, profilePictureUrl, createdAt, updatedAt } = action.payload;
            state.id = id;
            state.name = name;
            state.email = email;
            state.profilePictureUrl = profilePictureUrl ? profilePictureUrl : '';
            state.createdAt = createdAt;
            state.updatedAt = updatedAt;
        },
        clearUserInfo: (state) => {
            state.id = '';
            state.name = '';
            state.email = '';
            state.profilePictureUrl = '';
            state.createdAt = new Date();
            state.updatedAt = new Date();
        },
        deleteUserInfo: (state) => {
            state.id = '';
            state.name = '';
            state.email = '';
            state.profilePictureUrl = ''
            state.createdAt = new Date();
            state.updatedAt = new Date();
        }
    },
});

export const { setUserInfo, clearUserInfo, deleteUserInfo } = userInfoSlice.actions;
export default userInfoSlice.reducer;
