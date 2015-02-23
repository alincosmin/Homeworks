(deffacts printfacts)

(defrule powercable 
	(cantprint)
	(not (redlight))
	(noprinter)
	=>
	(printout t "Check power cable" crlf)
)

(defrule printercable
	(cantprint)
	(noprinter)
	=>
	(printout t "Check printer cable" crlf)
)

(defrule printersoftware
	(noprinter)
	=>
	(printout t "Check printer software" crlf)
)

(defrule checkink
	(redlight)
	=>
	(printout t "Check / replace ink" crlf)
)

(defrule paperjam
	(cantprint)
	(not (noprinter))
	=>
	(printout t "Check paper" crlf)
)