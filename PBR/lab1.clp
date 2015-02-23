(deffacts printfacts (cantprint 1) (redlight 1) (noprinter 1))

(defrule regula-yyy 
	(cantprint 1)
	(redlight 1)
	(noprinter 1)
	=>
	(printout t "Check ink" crlf)
	(printout t "Check printer cable" crlf)
	(printout t "Check printer software" crlf)
)

(defrule regula-yyn 
	(cantprint 1)
	(redlight 1)
	(noprinter 0)
	=>
	(printout t "Check paper" crlf)
	(printout t "Check ink" crlf)
)

(defrule regula-yny 
	(cantprint 1)
	(redlight 0)
	(noprinter 1)
	=>
	(printout t "Check power cable" crlf)
	(printout t "Check printer cable" crlf)
	(printout t "Check printer software" crlf)
)

(defrule regula-ynn 
	(cantprint 1)
	(redlight 0)
	(noprinter 0)
	=>
	(printout t "Check paper" crlf)
)

(defrule regula-nyy 
	(cantprint 0)
	(redlight 1)
	(noprinter 1)
	=>
	(printout t "Check ink" crlf)
	(printout t "Check printer software" crlf)
)

(defrule regula-nyn
	(cantprint 0)
	(redlight 1)
	(noprinter 0)
	=>
	(printout t "Check ink" crlf)
)

(defrule regula-nny
	(cantprint 0)
	(redlight 0)
	(noprinter 1)
	=>
	(printout t "Check printer software" crlf)
)