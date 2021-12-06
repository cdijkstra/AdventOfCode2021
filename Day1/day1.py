file = open('data.txt')
lines = file.readlines()

totalCount = 0
lineMinusTwo = 'Unknown'
LineMinusOne = 'Unknown'
previousSum = 'Unknown'

for line in lines:
    newLine = int(line)
    if lineMinusTwo != 'Unknown' and LineMinusOne != 'Unknown':
        sum = newLine + LineMinusOne + lineMinusTwo
        if previousSum != 'Unknown':
            if sum - previousSum > 0:
                totalCount += 1
        previousSum = sum

    lineMinusTwo = LineMinusOne
    LineMinusOne = newLine
    
print(totalCount)