
#-------------Setup----------------

import Ed

Ed.EdisonVersion = Ed.V2

Ed.DistanceUnits = Ed.CM
Ed.Tempo = Ed.TEMPO_MEDIUM

#--------Your code below-----------
#Note: this program simply reacts to the messages sent by the pen control Edison.
#Program the Edison controlling the paper with this program.
#This program should not need any modification to allow the printer to draw different shapes.

#Press 'play' on this robot BEFORE you press 'play' on the pen-control Edison.


#event handler for event 'printer_receive' - constantly monitoring for the event
Ed.RegisterEventHandler(Ed.EVENT_IR_DATA, "printer_receive")
#forever
while True:
    pass

#definition for 'printer_receive()' function, reads the message received from the pen control Edison, if any
def printer_receive():
    message = Ed.ReadIRData()
    
    #check for direction flags (set by pen control Edison)
    if message>64:
        #"Drive forwards" flag found. Remove the flag from the message
        message = message-64
        #drive the requested distance
        Ed.Drive(Ed.FORWARD, 1, message)
        #send a message to indicate the drive is complete
        Ed.SendIRData(5)
    elif message>32:
        #"Drive backwards" flag found. Remove the flag from the message
        message = message-32
        #drive the requested distance
        Ed.Drive(Ed.BACKWARD, 1, message)
        #send a message to indicate the drive is complete
        Ed.SendIRData(5)

