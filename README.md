# Szinkep

A program feladata hogy egy szövegfájlból beolvassa egy kép képpontjainak a színeit és azokkal különféle műveleteket végezzen.

## A bemeneti fájl formátuma
A forrásfájlban minden sor egy képpont színének az összetevőit tartalmazza, egymástól szóközzel elválasztva. A szín összetevőinek a sorrendje: `Piros Zöld Kék`.
Minta egy képpont színének a leírására:

    200 96 64

## Kimenet
A program kimenete a konzolon látható, valamint létrehoz egy `eredeti.bmp` nevű fájlt, mely tartalmazza a beolvasott szövegfájl által leírt képet, valamint egy `keretes.bmp` nevű fájlt, mely ugyan úgy a beolvasott fájlból létrehozott képet tartalmazza egy három pixel széles fekete kerettel, úgy hogy a kép mérete ugyan úgy `50*50` pixel mint a bemeneti fájlban leírt képé.