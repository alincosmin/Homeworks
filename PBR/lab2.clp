 (deftemplate piesa
	(slot nume)
	(slot artist)
 )

 (deffacts startFacts (piesa (nume abc) (artist cineva)) (piesa (nume def) (artist altcineva)))

 (defrule defaultOpt
 	(not (optiune $?))
 	=>
 	(printout t "Alege optiune:" crlf)
 	(printout t "1. Adaugare piesa" crlf)
 	(printout t "2. Listare piese" crlf)
 	(printout t "3. Listare dupa nume" crlf)
 	(printout t "4. Listare dupa artist" crlf)
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
	(not (piesa (nume nil)))
	?song<-(piesa (artist nil))
	=>
	(printout t "Artist: ")
	(modify ?song (artist (read)))
	(printout t "Introdus." crlf)
)

(defrule listSongs
	(optiune 2)
	(piesa (nume ?nume) (artist ?artist))
	=>
	(printout t "Nume: " ?nume " Artist: " ?artist crlf)
)

(defrule stopListing
	?id<-(optiune 2)
	=>
	(printout t "" crlf)
	(retract ?id)
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

(defrule listSongsByName
	(optiune 3)
	(search ?term)
	(piesa (nume ?name) (artist ?art))
	(test (str-index ?term ?name))
	=>
	(printout t "Nume: " ?name " Artist: " ?art crlf)
)

(defrule stopListingByName
	?id<-(optiune 3)
	?sid<-(search ?)
	=>
	(retract ?id)
	(retract ?sid)
	(printout t "" crlf)
)

(defrule listSongsByArtist
	(optiune 4)
	(search ?term)
	(piesa (artist ?art) (nume ?name))
	(test (str-index ?term ?art))
	=>
	(printout t "Artist: " ?art " Nume: " ?art crlf)
)

(defrule stopListingByArtist
	?id<-(optiune 4)
	?sid<-(search ?)
	=>
	(retract ?id)
	(retract ?sid)
	(printout t "" crlf)
)