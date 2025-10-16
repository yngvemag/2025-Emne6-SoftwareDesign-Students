# Hva er CSRF-angrep?

CSRF er en type angrep der en ondsinnet nettside urer brukeren til å utføre uønskede handlinger på en annen nettside hvor brukeren er innlogget. For eksempel:

1. Du er innlogget på bankens nettside
2. Du besøker en ondsinnet nettside som inneholder skjult kode
3. Denne koden sender en forespørsel til banken for å overføre penger
4. Siden du allerede er innlogget, blir forespørselen godkjent

## Hvordan fungerer Antiforgery?

Antiforgery-beskyttelsen fungerer ved å:

1. Generere tokens: Lager unike, tilfeldige tokens for hver bruker-sesjon
2. Validere tokens: Sjekker at tokens matcher når skjemaer sendes inn
3. Blokkere ugyldige forespørsler: Avviser forespørsler uten gyldig token

## I din Blazor-app

gjør at:

- Alle POST, PUT, DELETE-forespørsler må ha gyldig antiforgery-token
- Blazor-komponenter som bruker @rendermode InteractiveServer får automatisk beskyttelse
- Skjemaer må inkludere antiforgery-tokens

## Praktisk eksempel i Blazor

Kort sagt:

**app.UseAntiforgery();** beskytter applikasjonen din mot at ondsinnet kode på andre nettsider kan utføre handlinger på vegne av brukerne dine.******