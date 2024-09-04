import { toast } from "@/components/ui/use-toast";
import { RootState } from "@/redux/store";
import { createSlice } from "@reduxjs/toolkit";
import { fetchSubject } from "./thunks";

export interface SubjectsState {
  subjects: {
    [key: string]: Subject;
  };
}

const initialState: SubjectsState = {
  subjects: {},
};

export const subjectSlice = createSlice({
  name: "subjects",
  initialState,
  reducers: {
    resetSubjects: () => {
      return initialState;
    },
  },
  extraReducers: (builder) => {
    builder.addCase(fetchSubject.fulfilled, (state, action) => {
      if (action.payload) state.subjects = action.payload;
    });
    builder.addCase(createSubject.fulfilled, (state, { payload }) => {
      state.subjects[payload.id] = payload;
      toast({
        variant: "success",
        title: "Matéria criada com sucesso",
      });
    });
  },
});

export const { resetSubjects } = subjectSlice.actions;

export const selectSubjectOptions = (state: RootState) => {
  const subjects = state.subjectsReducer.subjects;
  return Object.keys(subjects).map((key) => ({
    value: subjects[key].id,
    label: subjects[key].name,
  }));
};

export default subjectSlice.reducer;
