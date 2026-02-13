window.downloadFile = function (filename, base64) {
    const link = document.createElement('a');
    link.href = 'data:image/png;base64,' + base64;
    link.download = filename;
    link.click();
};