IMAGE_STREAMGER = {
    UpdateImg: async function (elId, imgStream) {
        const arr = await imgStream.arrayBuffer();
        const blob = new Blob([arr]);
        const url = URL.createObjectURL(blob);
        const img = document.getElementById(elId);

        img.onload = () => { URL.revokeObjectURL(url); }
        img.src = url;
    },

    Save: (function () {
        var a = document.createElement("a");
        document.body.appendChild(a);
        a.style = "display: none";
        return async function (stream, fileName) {
            const arr2 = await stream.arrayBuffer();
            const blob2 = new Blob([arr2]);
            const url2 = window.URL.createObjectURL(blob2);
            a.href = url2;
            a.download = fileName;
            a.click();
            window.URL.revokeObjectURL(url2);
        };
    }()),

    ImgUrl: async function (imgStream) {
    const arr3 = await imgStream.arrayBuffer();
    const blob3 = new Blob([arr3]);
    const url3 = URL.createObjectURL(blob3);
    return url3;
    },

    RevokeUrl: (url3) => { URL.revokeObjectURL(url3); }
}




