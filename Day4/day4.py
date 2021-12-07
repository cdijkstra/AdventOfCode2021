lines = open('dummydata.txt', 'r').readlines()

bingoInput = lines[0]

allboards = []
board = []

for boardLine in range(2, len(lines)):
    board.append(lines[boardLine].strip())
    print(board)
    if lines[boardLine].strip() == '':
        allboards.append(board)
        board = []

# Add last board
allboards.append(board)

print(allboards[0][0][0])