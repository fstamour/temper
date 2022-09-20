
assembler := ../*.fs

%.bin: %.fs .. $(assembler)
	gforth $<

%.hex: %.bin
	avr-objcopy -I binary -O ihex $< $@

%.vcd: %.hex
	simavr --mcu attiny13 -f 9600000 -ff $< --output $@
