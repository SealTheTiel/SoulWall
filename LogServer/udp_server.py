import socket
import threading
import csv
import time
from pathlib import Path

BROADCAST_PORT = 9001
DATA_PORT = 9000

DATA_DIR = f"Data/{time.strftime('%Y-%m-%d_%H-%M-%S')}"
Path(DATA_DIR).mkdir(parents=True, exist_ok=True)

def getLocalIp():
    s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    s.connect(("8.8.8.8", 80))
    ip = s.getsockname()[0]
    s.close()
    return ip

SERVER_IP = getLocalIp()

def broadcastIp():
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    sock.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)
    
    while True:
        message = f"SERVER_IP:{SERVER_IP}".encode()
        sock.sendto(message, ("<broadcast>", BROADCAST_PORT))
        time.sleep(5)

def writeData(data):
    filename = data[0]
    data = data[1:]
    file = open(f"{DATA_DIR}/{filename}.csv", mode='a', newline='')
    writer = csv.writer(file)
    writer.writerow(data)
    file.close()

def setupFiles():
    file = open(f"{DATA_DIR}/modal_transform.csv", mode='w', newline='')
    writer = csv.writer(file);
    writer.writerow(["Timestamp", "Modal ID", "Position X", "Position Y", "Position Z", "Rotation X", "Rotation Y", "Rotation Z", "Scale X", "Scale Y", "Scale Z"])
    file.close()

    file = open(f"{DATA_DIR}/modal_actions.csv", mode='w', newline='')
    writer = csv.writer(file);
    writer.writerow(["Timestamp", "Modal ID", "Action"])
    file.close()

    file = open(f"{DATA_DIR}/eagle_focus.csv", mode='w', newline='')
    writer = csv.writer(file);
    writer.writerow(["Timestamp", "Eagle ID", "Focus"])
    file.close()



def receiveData():
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    sock.bind((SERVER_IP, DATA_PORT))

    while True:
        data, addr = sock.recvfrom(1024)
        data = data.decode().split(",")
        writeData(data)

setupFiles()
threading.Thread(target=broadcastIp, daemon=True).start()
threading.Thread(target=receiveData, daemon=True).start()

while True:
    time.sleep(1)
