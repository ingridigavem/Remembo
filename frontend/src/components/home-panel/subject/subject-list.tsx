import { fetchSubjects } from "@/redux/features/subjects/thunks";
import { useAppDispatch, useAppSelector } from "@/redux/hooks";
import { Library } from "lucide-react";
import { useEffect, useState } from "react";
import { CreateSubject } from "./create-subject";
import { SubjectDetail } from "./subject-detail";
import { RemoveSubject } from "./remove-subject";

export function SubjectList() {
  const dispatch = useAppDispatch();
  const subjects = useAppSelector((state) => state.subjectsReducer.subjects);
  const countSubjects = Object.keys(subjects).length;
  const [open, setOpen] = useState(false);
  const [selectedSubject, setSelectedSubject] = useState<Subject | undefined>();

  const selectSubject = (subject: Subject) => {
    setSelectedSubject(subject);
    setOpen(true);
  };

  useEffect(() => {
    dispatch(fetchSubjects());
  }, []);

  return (
    <>
      <SubjectDetail subject={selectedSubject} open={open} setOpen={setOpen} />

      <ul className="space-y-2 pt-8">
        {countSubjects == 0 && (
          <div className="text-center">
            <Library className="mx-auto h-12 w-12 text-muted-foreground" />
            <h3 className="mt-2 text-sm font-semibold ">Nenhuma matéria</h3>
            <p className="mt-1 text-sm text-muted-foreground">
              Comece criando uma matéria.
            </p>
            <div className="mt-6">
              <CreateSubject />
            </div>
          </div>
        )}
        {countSubjects > 0 &&
          Object.keys(subjects).map((key) => (
            <li
              key={`subject-${key}`}
              className="relative rounded-xl border bg-card text-card-foreground shadow hover:cursor-pointer hover:bg-accent"
            >
              <div
                className="p-6 flex items-center space-x-6"
                onClick={() => selectSubject(subjects[key])}
              >
                <div className="w-full flex items-center justify-between">
                  <div>
                    <p>{subjects[key].name}</p>
                  </div>
                </div>
              </div>

              <div className="z-10 absolute right-10 top-6">
                <RemoveSubject subject={subjects[key]} />
              </div>
            </li>
          ))}
      </ul>
    </>
  );
}
