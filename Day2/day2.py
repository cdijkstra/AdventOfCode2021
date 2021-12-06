file = open('data.txt')
lines = file.readlines()

horPosition = 0
depth = 0
aim = 0
for line in lines:
    instruction = line.split()[0]
    number = int(line.split()[1])
    if instruction == 'forward':
        horPosition += number
        depth += (number * aim)
    elif instruction == 'up':
        aim -= number
    elif instruction == 'down':
        aim += number
    
print(horPosition * depth)