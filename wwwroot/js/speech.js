document.addEventListener("DOMContentLoaded", () => {
    const speechBtn = document.getElementById("speechBtn");
    const speechStatus = document.getElementById("speechStatus");

    if (!('webkitSpeechRecognition' in window) && !('SpeechRecognition' in window)) {
        if (speechStatus) speechStatus.innerText = "Web Speech API no soportada en este navegador.";
        if (speechBtn) speechBtn.style.display = "none";
        return;
    }

    const SpeechRecognition = window.SpeechRecognition || window.webkitSpeechRecognition;
    const recognition = new SpeechRecognition();
    recognition.lang = 'es-ES';
    recognition.continuous = false;
    recognition.interimResults = false;

    speechBtn.addEventListener("click", () => {
        recognition.start();
        speechStatus.innerText = "Escuchando comando ('ordenar', 'reproducir')...";
        speechBtn.classList.add("listening");
    });

    recognition.onresult = (event) => {
        const transcript = event.results[0][0].transcript.toLowerCase();
        speechStatus.innerText = `Comando: "${transcript}"`;
        speechBtn.classList.remove("listening");

        if (transcript.includes("ordenar")) {
            document.getElementById("sortForm").submit();
        } else if (transcript.includes("reproducir")) {
            alert("Reproduciendo playlist en modo simulador Spotify.");
        }
    };

    recognition.onerror = () => {
        speechStatus.innerText = "Error en el reconocimiento de voz.";
        speechBtn.classList.remove("listening");
    };
})