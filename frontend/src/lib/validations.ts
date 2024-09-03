export const passwordValidation = new RegExp(
    /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/
);

export const fullNameValidation = new RegExp(
    /^[a-zA-Z]+\s+[a-zA-Z]+$/
);

export const onlyLettersValidation = new RegExp(
    /^[a-zA-Z]+[-'s]?[a-zA-Z ]+$/
);
