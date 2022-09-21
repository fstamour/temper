
assembler := $(wildcard ../*.fs)
examples := $(wildcard *.fs)
traces := $(patsubst %.fs, %.vcd, $(examples))
disassemblies := $(patsubst %.fs, %.asm, $(examples))

all: $(disassemblies) # $(traces)

# Assemble
%.bin: %.fs $(assembler)
	gforth $<

# Disassamble
%.asm: %.bin
	avr-objdump -b binary -D $< -m avr25 > $@

# Convert binary format to ihex format, as some tools don't take
# binary format as input
%.hex: %.bin
	avr-objcopy -I binary -O ihex $< $@

.PHONY: clean
clean:
	rm -f *.bin *.hex *.asm
