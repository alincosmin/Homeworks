 (deftemplate piesa
	(slot nume)
	(slot artist)
	(slot an)
 )

 (deffacts startFacts (piesa (nume abc) (artist cineva) (an 2014)) (piesa (nume def) (artist altcineva) (an 2015)) (year 9999))

 (defrule defaultOpt
 	(not (optiune $?))
 	=>
 	(printout t "Alege optiune:" crlf)
 	(printout t "1. Adaugare piesa" crlf)
 	(printout t "2. Listare piese (ordonate)" crlf)
 	(printout t "3. Cautare dupa nume" crlf)
 	(printout t "4. Cautare dupa artist" crlf)
 	(printout t "5. Cautare dupa an" crlf)
 	(assert (optiune (read)))
 )

(defrule addSong
	?id<-(optiune 1)
	=>
	(retract ?id)
	(printout t "Introducere piesa..." crlf)
	(assert (piesa))
)

(defrule addSongName
	?song<-(piesa (nume nil))
	=>
	(printout t "Denumire: ")
	(modify ?song (nume (read)))
)

(defrule addSongArtist
	?song<-(piesa (artist nil))
	=>
	(printout t "Artist: ")
	(modify ?song (artist (read)))
)

(defrule addSongYear
	?song<-(piesa (an nil))
	=>
	(printout t "Anul: ")
	(bind ?year (read))
	(modify ?song (an ?year))
	(printout t "Introdus." crlf)
)

(defrule listSongs
	(optiune 2)
	(piesa (nume ?nume) (artist ?artist) (an ?an))
	=>
	(printout t "Nume: " ?nume " Artist: " ?artist " Anul: " ?an crlf)
)

(defrule orderedList
	(piesa (nume ?name) (an ?year))
	(not (exists (piesa (an ?y&:(< ?y ?year)))))
	=>
	(printout t "" ?name " - " ?year crlf)
)

(defrule prepareNameSearch
	(optiune 3)
	=>
	(printout t "Numele cautat: ")
	(assert (search (read)))
)

(defrule prepareArtistSearch
	(optiune 4)
	=>
	(printout t "Artistul cautat: ")
	(assert (search (read)))
)

(defrule prepareYearSearch
	(optiune 5)
	=>
	(printout t "Anul cautat: ")
	(assert (search (read)))
)

(defrule listSongsByName
	(optiune 3)
	(search ?term)
	(piesa (nume ?name) (artist ?art) (an ?an))
	(test (str-index ?term ?name))
	=>
	(printout t "Nume: " ?name " Artist: " ?art " Anu: " ?an crlf)
)

(defrule listSongsByArtist
	(optiune 4)
	(search ?term)
	(piesa (nume ?name) (artist ?art) (an ?an))
	(test (str-index ?term ?art))
	=>
	(printout t "Artist: " ?art " Nume: " ?name " Anul: " ?an crlf)
)

(defrule listSongsByYear
	(optiune 5)
	(search ?term)
	(piesa (an ?year) (artist ?art) (nume ?name))
	(test (= ?term ?year))
	=>
	(printout t "Anul: " ?year " Artist: " ?art " Nume: " ?name crlf)
)

(defrule stopListing
	?id<-(optiune ?)
	?sid<-(search ?)
	=>
	(retract ?id)
	(retract ?sid)
	(printout t "" crlf)
)
