export function getBase64(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => {
        const base64String = reader.result.split(',')[1];
        const mimeType = reader.result.split(',')[0].split(':')[1].split(';')[0];
        resolve({ base64String, mimeType });
        };
        reader.onerror = (error) => reject(error);
    });
}  