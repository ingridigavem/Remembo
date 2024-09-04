export function titleCase(str: string): string {
    const splitStr = str.toLowerCase().split(' ')

    for (let i = 0; i < splitStr.length; i++) {
        splitStr[i] = splitStr[i].charAt(0).toUpperCase() + splitStr[i].substring(1);
    }
    return splitStr.join(' ');
}

export function getAcronym(str: string): string {
    const matches = str.match(/\b(\w)/g);
    if (matches)
        return matches.join('').toUpperCase();

    return "ND"
}
