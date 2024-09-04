import dashboardReducer from "@/redux/features/dashboard/dashboardSlice";
import subjectsReducer from "@/redux/features/subjects/subjectSlice";
import userReducer from "@/redux/features/user/userSlice";

import { combineReducers } from "@reduxjs/toolkit";

export default combineReducers({
  userReducer,
  subjectsReducer,
  dashboardReducer,
});
