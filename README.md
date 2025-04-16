# WpfADS - Communication TwinCAT3 ↔ C# (WPF)

Ce projet permet de réaliser des échanges de données entre une application WPF en C# et une application **TwinCAT 3** via le protocole **ADS**, en utilisant la bibliothèque `TwinCAT.Ads`.

## 🧰 Fonctionnalités

- Lecture des variables TwinCAT (int, string, bool, double)
- Écriture depuis l'IHM vers TwinCAT
- Rafraîchissement automatique toutes les 50 ms
- Interface utilisateur simple avec WPF
- Gestion des erreurs d’accès ADS

## 🖼️ Aperçu

L'application WPF permet de :
- Lire des variables déclarées dans le code PLC
- Modifier dynamiquement leur valeur depuis l’interface
- Voir l’évolution en temps réel via un horodatage mis à jour régulièrement

## 🔧 Prérequis

- Visual Studio 2022 (ou plus récent)
- .NET Framework 4.7.2 (ou supérieur)
- TwinCAT 3 installé et configuré
- Variables déclarées dans votre programme PLC TwinCAT :


PROGRAM MAIN
VAR
    nCounter : INT;
    sString1 : STRING(80);
    bCheckBox : BOOL;
    lrealVal : LREAL;
END_VAR
