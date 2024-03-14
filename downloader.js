export function download(file, bytesBase64) {
    var link = document.createElement('a');
    link.download = file;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}