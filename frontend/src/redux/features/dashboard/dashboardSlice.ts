import { toast } from "@/components/ui/use-toast"
import { stringToDateFormatted } from "@/lib/date"
import { filterContentsOnComming, filterContentsOverdue } from "@/lib/utils"
import { RootState } from "@/redux/store"
import { createSlice } from "@reduxjs/toolkit"
import { createContent, fetchDashboard } from "./thunk"

export interface DashboardState {
    statistics: Statistics
    subjects: {
        [key: string] : SubjectWithContent
    }
}

const initialState: DashboardState = {
    statistics: {
        completedReviewsTotal: 0,
        completedContentTotal: 0,
        notCompletedContentTotal: 0
    },
    subjects: {},
}

export const dashboardSlice = createSlice({
    name: 'dashboard',
    initialState,
    reducers: {
        resetDashboard: () => {
            return initialState
        }
    },
    extraReducers: (builder) => {
        builder.addCase(fetchDashboard.fulfilled, (state, action) => {
            if(action.payload) {
                if(action.payload.statistics)
                    state.statistics = action.payload.statistics
                if(action.payload.subjects)
                    state.subjects = action.payload.subjects
            }
        })
        builder.addCase(createContent.fulfilled, (state, { payload }) => {
            const newContentReview = payload.subject.contentReview

            state.subjects[payload.subject.subjectId] = {
                ...state.subjects[payload.subject.subjectId],
                contents: [
                    ...state.subjects[payload.subject.subjectId]?.contents ?? [],
                    {
                        ...newContentReview
                    }
                ]
            }
            toast({
                variant: "success",
                title: "Conteúdo criado com sucesso",
                description: `A sua próxima revisão deste conteúdo será dia ${stringToDateFormatted(newContentReview.currentReview.scheduleReviewDate)}`
            })
        })
    },
})

export const { resetDashboard } = dashboardSlice.actions;

export const selectSubjectContentsOnComming = (state: RootState) => {
    const subjects = state.dashboardReducer.subjects

    return Object.keys(subjects).map(key => {
        const subject = subjects[key]
        return {
            ...subject,
            contents: filterContentsOnComming(subject.contents)
        }
    })
}

export const selectCountContentsOnComming = (state: RootState) => {
    const subjects = selectSubjectContentsOnComming(state)

    return subjects.reduce((accumulator, subject) => accumulator + subject.contents.length, 0);
}

export const selectSubjectContentsOverdue = (state: RootState) => {
    const subjects = state.dashboardReducer.subjects

    return Object.keys(subjects).map(key => {
        const subject = subjects[key]
        return {
            ...subject,
            contents: filterContentsOverdue(subject.contents)
        }
    })
}

export const selectCountContentsOverdue = (state: RootState) => {
    const subjects = selectSubjectContentsOverdue(state)

    return subjects.reduce((accumulator, subject) => accumulator + subject.contents.length, 0);
}

export default dashboardSlice.reducer
