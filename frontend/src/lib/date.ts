export const dateFormatter = new Intl.DateTimeFormat('pt-BR')

export const stringToDateFormatted = (val: string) => {
    return dateFormatter.format(Date.parse(val))
}
