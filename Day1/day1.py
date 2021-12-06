file = open('data.txt')
lines = file.readlines()

totalCount = 0
previousLine = 'Unknown'
for line in lines:
    intLine = int(line)
    if previousLine != 'Unknown' and intLine - previousLine > 0:
        totalCount += 1
    previousLine = intLine
    
print(totalCount)