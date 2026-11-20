# Tjugoett – C# Console Game

## Om projektet

Tjugoett är ett kortspel utvecklat i C# som körs i en konsolapplikation. Spelet går ut på att få en hand med ett värde så nära 21 som möjligt utan att överstiga 21.

Spelarna tävlar mot givaren.

## Funktioner

- Stöd för 1–5 spelare.
- En kortlek med 52 kort.
- Kortleken blandas med Fisher–Yates-algoritmen.
- Ess kan räknas som 1 eller 11.
- Klädda kort (knekt, dam och kung) är värda 10 poäng.
- Givaren drar kort vid 16 poäng eller lägre och stannar vid 17 eller högre.
- Naturlig tjugoett ger särskild vinst.
- Statistik över vinster, förluster och oavgjorda rundor.
- Spelarnas statistik sparas i en JSON-fil.
- ASCII-kortgrafik visas i konsolen.

## Spelregler

1. Varje spelare får två kort.
2. Givaren får också två kort, varav ett är dolt under spelarnas turer.
3. Spelarna väljer om de vill ta ett kort eller stanna.
4. Om en spelare får mer än 21 poäng förlorar spelaren rundan.
5. Givaren drar kort tills handen har minst 17 poäng.
6. Den spelare som har högst poäng utan att överstiga 21 vinner.
7. Naturlig tjugoett består av exakt två kort med värdet 21.

## Tekniker

- C#
- .NET 10
- JSON för sparning av spelarstatistik
- Git och GitHub för versionshantering

## Så startar du spelet

1. Klona repository från GitHub.
2. Öppna projektmappen i Visual Studio Code.
3. Öppna terminalen.
4. Kör följande kommando:

```bash
dotnet run
```

## GitHub

[https://github.com/td2k5/Tjugoett](https://github.com/td2k5/Tjugoett)
