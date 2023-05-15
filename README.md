# WordFinder

El objetivo de esta soluci�n es resolver el ejercicio de WordFinder.
Se implementan 2 alternativas.

## WordFinder

En esta soluci�n se genera una matriz con las palabras verticales. De esta forma podemos recorrer ambas matrices(horizontal y vertical) para buscar la coincidencias.
Para buscar las coincidencias se usa ```IndexOf``` para saber si la palabra ingresada est� contenida en la palabra de la matriz.

### WordFinderAlt

En esta soluci�n recorremos los caracteres de cada palabra de la matriz buscando una coincidencia con el primer caracter de la palabra. Una vez encontrado se comienza a analizar de forma horizontal y vertical los siguientes caracteres hasta finalizar la palabra para determinar la coincidencia.