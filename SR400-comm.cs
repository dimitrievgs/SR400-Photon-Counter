using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_400
{
    public class Instrument
    {
        public Instrument(string name, string address, bool reset = false)
        {

        }

        public void init(string name)
        {

        }
    }

    public class VisaInstrument
    {
        public VisaInstrument(string address)
        {

        }
    }


    public class SR_400_comm : Instrument //class SR_400(Instrument):
    {
        const int SCALE_LOG = 0;
        const int SCALE_987 = 1;
        const int SCALE_876 = 2;
        const int SCALE_765 = 3;
        const int SCALE_654 = 4;
        const int SCALE_543 = 5;
        const int SCALE_432 = 6;
        const int SCALE_321 = 7;

        public SR_400_comm(string name, string address, bool reset = false): base(name, address, reset)
        {
            base.init(name);

            var _address = address;
            var _visa = new VisaInstrument(_address);

            add_parameter('identification',
                flags = Instrument.FLAG_GET);

            add_parameter('mode',
                flags = Instrument.FLAG_GETSET,
                type = types.IntType,
                minval = 0, maxval = 3,
                format_map ={
                0: 'A, B for preset T',
                1: 'A - B for preset T',
                2: 'A + B for preset T',
                3: 'A for preset B',
            },
            doc = """Get / set mode""");

            add_parameter('counter',
                flags = Instrument.FLAG_GET,
                type = types.IntType,
                channels = ('A', 'B'));

            add_parameter('count',
                flags = Instrument.FLAG_GET,
                channels = ('A', 'B', 'T'),
                doc = """


                Start counting and return data(all n periods).
                Channel A, B, or T(= A and B as tuple)
                """);


        add_parameter('counter_input',
            flags = Instrument.FLAG_GETSET,
            type = types.IntType,
            channels = ('A', 'B', 'T'),
            minval = 0, maxval = 3,
            format_map = {
                0: '10MHz',
                1: 'Input 1',
                2: 'Input 2',
                3: 'Trigger'
            },
            doc = """Get / set input""");

            add_parameter('counter_preset',
                flags = Instrument.FLAG_GETSET,
                type = types.IntType,
                channels = ('B', 'T'),
                minval = 1, maxval = 9e11,
                format = '%1.1e',
                doc = """


                Get / set preset count for a channel, 1 <= n <= 9e11.


                If input is 10MHz the units are 100ns periods,
                only one significant digit can be used.


                """);



            add_parameter('periods',
                flags = Instrument.FLAG_GETSET,
                type = types.IntType,
                minval = 1, maxval = 2000,
                doc = """


                Get / set number of periods to measure.


                """);



            add_parameter('current_period',
                flags = Instrument.FLAG_GET,
                type = types.IntType,
                doc = """


                Get current period number.


                """);



            add_parameter('disc_slope',
                flags = Instrument.FLAG_GETSET,
                type = types.IntType,
                channels = ('A', 'B', 'T'),
                format_map ={
                0: 'RISE',
                1: 'FALL',
            },
            minval = 0, maxval = 1,
            doc = """Get/set discriminator scope""");

            add_parameter('disc_level',
                flags = Instrument.FLAG_GETSET,
                type = types.FloatType,
                channels = ('A', 'B', 'T'),
                minval = -0.3, maxval = 0.3,
                units = 'V',
                doc = """


                Get / set discriminator level, -0.3V < 0 < 0.3V


                """)


        if reset
        {
                reset();
            }
            else
            {
                get_all();
            }
        }

        public void _counter_num( counter)
        {
            if (type(counter) == types.IntType)
                return counter;
            elif type(counter) == types.StringType
            {
                nummap = { 'A': 0, 'B': 1, 'T': 2}
                return nummap.get(counter.upper(), None);
            }
        else
            {
                return None;
            }
        }

        public void reset()
        {
            _visa.write('*RST');
        }

        public void get_all()
        {
            get_mode();
            get_periods();
            for chan in 'A', 'B'
            {
                get('counter%s' % chan);
                get('counter_input%s' % chan);
                get('disc_level%s' % chan);
                get('disc_slope%s' % chan);
            }

            get('counter_presetB');
            get('counter_presetT');
        }

        public void do_get_identification()
        {
            return var _visa.ask('*IDN?');
        }

        public void do_get_mode()
        {
            ans = var _visa.ask('CM')
            return ans;
        }

        public void do_set_mode(mode)
        {
            _visa.write('CM %d' % mode);
        }

        public void do_get_counter(channel)
        {
            ans = var _visa.ask('Q%s' % channel);
            return int(ans);
        }

        public void do_get_count(channel)
        {
            ans = var _visa.ask('CR; F%s' % channel);
            if (channel == 'T')
            {
                ans2 = _visa.read();
                return int(ans), int(ans2);
            }
            else
            {
                return int(ans);
            }
        }

        public void do_get_counter_input(channel)
        {
            ans = var _visa.ask('CI %d' % var _counter_num(channel));
            return int(ans);
        }

        public void do_set_counter_input(val, channel)
        {
            _visa.write('CI %d,%d' % (var _counter_num(channel), val));
        }

        public void do_get_counter_preset(channel)
        {
            ret = var _visa.ask('CP %d' % var _counter_num(channel));
            return float(ret);
        }

        public void do_set_counter_preset(val, channel)
        {
            _visa.write('CP %d,%d' % (var _counter_num(channel), val));
        }

        public void do_get_periods()
        {
            ans = var _visa.ask('NP');
            return ans;
        }

        public void do_set_periods(val)
        {
            _visa.write('NP %d' % val);
        }

        public void do_get_disc_slope(channel)
        {
            ans = var _visa.ask('DS %d' % var _counter_num(channel));
            return ans;
        }

        public void do_set_disc_slope(val, channel)
        {
            _visa.write('DS %d,%d' % (var _counter_num(channel), val));
        }

        public void do_get_disc_level(channel)
        {
            ans = var _visa.ask('DL %d' % var _counter_num(channel));
            return ans;
        }

        public void do_set_disc_level(val, channel)
        {
            _visa.write('DL %d,%f' % (var _counter_num(channel), val));
        }

        public void do_set_periods(val)
        {
            _visa.write('NP %d' % val);
        }

        public void do_get_current_period()
        {
            ans = var _visa.ask('NN');
            return int(ans);
        }

        public void start()
        {
            //
            //Reset counter and start counting
            //
            _visa.write('CR; CS');
        }

        public void stop()
        {
            //
            //Stop counting
            //
            _visa.write('CS');
        }
    }
}