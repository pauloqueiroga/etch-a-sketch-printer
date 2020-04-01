#-------------Setup----------------

import Ed

Ed.EdisonVersion = Ed.V2
Ed.DistanceUnits = Ed.CM
Ed.Tempo = Ed.TEMPO_MEDIUM
#event handler for event 'message_receive' - constantly monitoring for the event
Ed.RegisterEventHandler(Ed.EVENT_IR_DATA, "message_receive" )
messageReceivedFlag = 1

#--- write stuff

drawLineLeft(15)
drawLineForward(3)
drawLineRight(10)
drawLineBackward(2)
drawLineLeft(5)
drawLineForward(1)
drawLineRight(5)

drawLineLeft(5)
drawLineBackward(1)
drawLineRight(5)
drawLineForward(2)
drawLineLeft(10)
drawLineBackward(3)
drawLineRight(15)
drawLineForward(4)

writeG(1)
writeA(1)
writeB(1)
writeI(1)

drawLineForward(3)
drawLineLeft(15)
drawLineBackward(3)
drawLineRight(10)
drawLineForward(2)
drawLineLeft(5)
drawLineBackward(1)
drawLineRight(5)

#Writing Symbols
def writeSpace(scale):
    drawLineForward(3 *scale)

#Writing Uppercase

def writeA(scale):
    drawLineLeft(6 *scale)
    drawLineForward(5 *scale)
    drawLineRight(3 *scale)
    drawLineBackward(5 *scale)
    drawLineForward(5 *scale)
    drawLineRight(3 *scale)
    drawLineForward(scale)

def writeB(scale):
    drawLineLeft(6 *scale)
    drawLineForward(4 *scale)
    drawLineRight(3 *scale)
    drawLineBackward(4 *scale)
    drawLineForward(5 *scale)
    drawLineRight(3 *scale)
    drawLineForward(scale)
    drawLineBackward(5 *scale)
    drawLineForward(6 *scale)

def writeC(scale):
    drawLineForward(1 *scale)
    drawLineLeft(1 *scale)
    drawLineBackward(1 *scale)
    drawLineLeft(4 *scale)
    drawLineForward(1 *scale)
    drawLineLeft(1 *scale)
    drawLineForward(4 *scale)
    drawLineBackward(4 *scale)
    drawLineRight(1 *scale)
    drawLineBackward(1 *scale)
    drawLineRight(4 *scale)
    drawLineForward(1 *scale)
    drawLineRight(1 *scale)
    drawLineForward(5 *scale)

def writeD(scale):
    drawLineLeft(6 *scale)
    drawLineForward(4 *scale)
    drawLineRight(1 *scale)
    drawLineForward(1 *scale)
    drawLineRight(4 *scale)
    drawLineBackward(1 *scale)
    drawLineRight(1 *scale)
    drawLineBackward(4 *scale)
    drawLineForward(6 *scale)

def writeG(scale):
    drawLineForward(1 *scale)
    drawLineLeft(1 *scale)
    drawLineBackward(1 *scale)
    drawLineLeft(4 *scale)
    drawLineForward(1 *scale)
    drawLineLeft(1 *scale)
    drawLineForward(4 *scale)
    drawLineBackward(4 *scale)
    drawLineRight(1 *scale)
    drawLineBackward(1 *scale)
    drawLineRight(4 *scale)
    drawLineForward(1 *scale)
    drawLineRight(1 *scale)
    drawLineForward(4 *scale)
    drawLineLeft(3 *scale)
    drawLineBackward(2 *scale)
    drawLineForward(2 *scale)
    drawLineRight(3 *scale)
    drawLineForward(1 *scale)

def writeI(scale):
    drawLineForward(1 *scale)
    drawLineLeft(6 *scale)
    drawLineRight(6 *scale)
    drawLineForward(1 *scale)

#Pen-control Edison base functions

#definition of 'drawLineLeft(numCM)' function, draw a line moving away from Edison
def drawLineLeft(numCM):
    #constrain input value
    if numCM > 15:
        numCM = 15
    #move pen
    Ed.Drive(Ed.FORWARD, 2, numCM)

#definition of 'drawLineRight(numCM)' function, draw a line moving towards Edison  
def drawLineRight(numCM):
    #constrain input value
    if numCM > 15:
        numCM = 15
    #move pen
    Ed.Drive(Ed.BACKWARD, 2, numCM)
    
#definition of 'drawLineForward(numCM)' function, move the paper to draw a line forwards on the paper    
def drawLineForward(numCM):
    #constrain input value
    if numCM > 15:
        numCM = 15
        
    #set send message with "drive forwards" flag
    sendValue = 64
    #Add distance to drive to the message, using a 'bitwise OR'
    sendValue = sendValue|numCM
    
    #send message to move paper to the paper-controlling Edison
    Ed.SendIRData(sendValue)
    #wait for the paper-controlling Edison to send a message to indicate it has stopped moving
    dataBack = 255;
    while dataBack != 5:
        dataBack= waitForMessage()

#definition of 'drawLineBackward(numCM)' function, move the paper to draw a line backwards on the paper
def drawLineBackward(numCM):
    #constrain input value
    if numCM > 15:
        numCM = 15
    
    #set send message with "drive backwards" flag
    sendValue = 32
    #add distance to drive to the message, using a 'bitwise OR'
    sendValue = sendValue|numCM
    
    #send message to move paper
    Ed.SendIRData(sendValue)
    #wait for the paper-controlling Edison to send a message to indicate it has stopped moving
    dataBack = 255;
    while dataBack != 5:
        dataBack= waitForMessage()

#definition of 'waitForMessage()' function, to wait for a message to be seen before returning the value of the sent message
def waitForMessage():
    global messageReceivedFlag
    while messageReceivedFlag==0:
        pass
    messageReceivedFlag=0
    return Ed.ReadIRData()

#definition of 'message_receive()' function, sets the message received flag when a new message has been received
def message_receive():
    global messageReceivedFlag
    messageReceivedFlag = 1