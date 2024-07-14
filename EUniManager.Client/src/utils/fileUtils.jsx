import PictureAsPdfIcon from '@mui/icons-material/PictureAsPdf';
import ImageIcon from '@mui/icons-material/Image';
import DescriptionIcon from '@mui/icons-material/Description';
import InsertDriveFileIcon from '@mui/icons-material/InsertDriveFile';

export function getIcon(type) {
    switch (type) {
      case '.pdf':
        return <PictureAsPdfIcon sx={{ color: '#ff0000', fontSize: 40 }} />;
      case '.png':
      case '.jpg':
      case '.gif':
      case '.tiff':
        return <ImageIcon sx={{ color: '#4caf50', fontSize: 40 }} />;
      case '.docx':
        return <DescriptionIcon sx={{ color: '#3b5998', fontSize: 40 }} />;
      case '.xslx':
        return <InsertDriveFileIcon sx={{ color: '#0d820d', fontSize: 40 }} />;
      case '.pptx':
        return <InsertDriveFileIcon sx={{ color: '#d24726', fontSize: 40 }} />;
      default:
        return <InsertDriveFileIcon sx={{ fontSize: 40 }} />;
    }
  };

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