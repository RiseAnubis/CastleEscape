# CastleEscape - Ein C#-Text-Adventure

CastleEscape ist ein einfaches Text-Adventure, in dem sich der Spieler durch verschiedene Räume bewegt und Items einsammelt.
Es besteht aus 2 Komponenten:
* CastleEscape - Das eigentliche Text-Adventure
* CastleEdit - Ein Level-Editor, mit man eigene Levels erstellen kann

## Befehle in CastleEscape
* Hilfe - Zeigt eine Befehlsliste an
* Umsehen - Zeigt den aktuellen Raum an
* Inventar - Zeigt das Inventar an
* Nimm (Item) - Hebt ein Item auf
* Ablegen (Item) - Legt ein Item wieder ab
* Gehe (Richtung) - Betritt den nächsten Raum
* Öffne (Richtung) - Öffnet eine Tür zu einem verschlossenen Raum, wenn man das entsprechende Item im Inventar hat
* Beenden - Beendet das Spiel

## Bedienung CastleEdit
* Rechtsklick in einen Raum zum Erstellen oder Löschen
* Raumeigenschaften werden im rechten Bereich des Editors bearbeitet
* Spiel-Items werden im linken Bereich erstellt oder gelöscht
* Um einem Raum ein Item hinzuzufügen, wird dieses per Drag&Drop von der Spiel-Item-Liste auf die Item-Liste des Raumes
(in den Raumeigenschaften) gezogen
* Ein Item wird aus einem Raum gelöscht, wenn man es markiert und ENTF drückt
* Im Raum-Viewer kann man zoomen entweder mit dem Schieberegler links unten oder mit STRG+Mausrad
* Vertikal scrollen kann man mit dem Mausrad, horizontal scrollen mit SHIFT+Mausrad
* Ein Doppelklick in den Viewer setzt den Zoom wieder auf den Standardwert zurück
* Levels kann man mit den entsprechenden Menübefehlen in der oberen Leiste laden oder speichern. Beim Speichern ist darauf zu achten,
dass unten in der Statusleiste eine gültige Startposition für den Spieler eingetragen ist (z.B. 0 und 0, was dem linken oberen
Raum entspricht). Zudem muss die Datei "Game.xml" heißen und im selben Verzeichnis abgespeichert werden, wo auch
das Spiel CastleEscape.exe liegt

## Bekannte Fehler bei CastleEdit
* Wenn man die Raumeigenschaften bearbeitet, ist noch nicht ersichtlich, dass auch wirklich der zuletzt ausgewählte Raum
bearbeitet wird, da die Markierung verloren geht, wenn man in die Raumeigenschaften springt
* Das Layout des Textes in den Eingabefeldern bei der Spielerposition ist nicht optimal
* Der Style in der Liste der Spiel-Items ist noch nicht final
* Tooltips werden fehlerhaft dargestellt, wenn man ein Item aus einem Raum löscht und anschließend wieder über den Raum fährt,
um die Items anzuzeigen
