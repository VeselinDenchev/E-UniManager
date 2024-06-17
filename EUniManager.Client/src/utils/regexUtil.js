export const containsOnlyDigits = (text) => {
    const regex = /^\d+$/;

    return regex.test(text);
}