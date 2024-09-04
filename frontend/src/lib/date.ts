export const dateForsubject = new Intl.DateTimeFormat('pt-BR')

export const stringToDateFormatted = (val: string) => {
    return dateForsubject.format(Date.parse(val))
}
