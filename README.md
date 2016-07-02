# bookmark-manager
A bookmark-manager built with umbraco

# Readme #

Dieses Package stellt einen Bookmark-Manager bereit.

**Connection-String amazonws**

<connectionStrings>
    <remove name="umbracoDbDSN" />
    <add name="umbracoDbDSN" connectionString="server=umbraco-ms.crciwz8iusid.us-west-2.rds.amazonaws.com;database=umbraco;user id=umbraco;password=umbraco-ms" providerName="System.Data.SqlClient" />
    <!-- Important: If you're upgrading Umbraco, do not clear the connection string / provider name during your web.config merge. -->
  </connectionStrings>

## Macros ##

Es sind Macros vorhanden zum

* Anlegen und Editieren von Bookmarks
* Anzeigen aller gespeicherten Bookmarks
* Anzeigen eines einzelnen Bookmarks
* Anlegen von Einfärberegeln für Tags
* Auflisten aller Einfärberegeln zum Löschen
* Auflisten aller Tags
* Login-Formular
* Registrierungsformular
* memberSettings-Formular

## Data Types ##

* Es gibt einen Datentyp für gefärbte Tags
* Für eine übersichtliche ListView der einzelnen Bookmarks wurde ein eigener Typ angelegt

## Struktur Content / Document Types ##

Im Content-Bereich muss eine Struktur für die folgenden Document Types eingehalten werden

- Bookmark Manager
  - Landing Page
  - Bookmarks Root  (Enthält einen Knoten je Member)
    - Bookmarks Member  (Enhält alle Bookmarks eines Members)
      - Bookmark
  - Bookmark Form
  - Tag Form
  - Member Settings

Dabei können die Makros jeweils frei in einem Grid-Layout angeordnet werden.

## Templates ##

* Master.cshtml
  * Master-Template für alle Partial-Makros
  * Enthält die Nav-Leiste und das RenderBody
* Landing-Page
  * Willkommenseite mit Login und Registrierung
* BookmarksPage
  * Enthält ein RenderGrid zum rendern der verschiedenen Content-Nodes

## Zugriff / Member-Bereich ##

* Es muss eine Member-Gruppe "BookmarksUser" angelegt werden
* Die jeweiligen Content-Nodes müssen über Public Access der Gruppe zugeordnet werden
* Neue Member werden automatisch der Member-Gruppe zugeordnet

## Skripte ##

* für die Tag-Liste im FrontEnd wird `bootstrap-tagsinput` verwendet
* Bootstrap mit jQuery wird verwendet

## Layout ##

* Für das Layout wird Bootstrap 3 verwendet
