file = open('data.txt')
lines = file.readlines()

totalCount = 0

for idx in range(1, len(lines) - 1): # Always consider N, but also N-1 and N+1
    if int(lines[idx + 1]) > int(lines[idx - 1]) > 0:
        totalCount += 1
    
print(totalCount)