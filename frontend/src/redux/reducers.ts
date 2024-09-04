
import dashboardReducer from '@/redux/features/dashboard/dashboardSlice';
import mattersReducer from '@/redux/features/matters/mattersSlice';
import userReducer from '@/redux/features/user/userSlice';

import { combineReducers } from '@reduxjs/toolkit';

export default combineReducers({
    userReducer,
    mattersReducer,
    dashboardReducer
})
